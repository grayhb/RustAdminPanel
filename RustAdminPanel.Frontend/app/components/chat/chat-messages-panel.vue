<template>
  <div class="panel_container">
    <v-form @submit.prevent="fetchData">
      <v-combobox
        label="Период"
        v-model="dateRangeVariant"
        :items="dateRangeVariants"
        variant="solo-filled"
        density="comfortable"
        hide-details
        @update:model-value="fetchData"
        :disabled="loading"
        :item-props="compactDensityProps"
      ></v-combobox>

      <v-combobox
        label="Канал"
        v-model="channelVariant"
        :items="channelVariants"
        variant="solo-filled"
        density="comfortable"
        hide-details
        class="mt-4"
        item-value="id"
        item-title="title"
        :return-object="false"
        clearable
        @update:model-value="fetchData"
        :disabled="loading"
        :item-props="compactDensityProps"
      ></v-combobox>

      <v-text-field
        label="Steam Id"
        v-model="steamId"
        variant="solo-filled"
        density="comfortable"
        hide-details
        :disabled="loading"
        class="mt-4"
        clearable
        @keydown.enter="fetchData"
      ></v-text-field>

      <v-text-field
        label="Steam Name"
        v-model="steamName"
        variant="solo-filled"
        density="comfortable"
        hide-details
        :disabled="loading"
        class="mt-4"
        clearable
        @keydown.enter="fetchData"
      ></v-text-field>

      <v-text-field
        label="Search"
        v-model="searchValue"
        variant="solo-filled"
        density="comfortable"
        hide-details
        :disabled="loading"
        class="mt-4"
        clearable
        @keydown.enter="fetchData"
      ></v-text-field>

      <v-btn
        variant="flat"
        color="primary"
        class="mt-4"
        @click="fetchData"
        block
      >
        Поиск
      </v-btn>
    </v-form>
  </div>
  <div class="panel_count-items">Найдено записей: {{ entites.length }}</div>
</template>

<script lang="ts" setup>
import { addDays, addMonths, addYears, formatISO } from "date-fns";

import { ON_WEEK, ON_MONTH, ON_YEAR, ON_DAY } from "~/constants";
import type { ChatMessageQuery } from "~/types/chat.types";

const dateRangeVariants = [ON_DAY, ON_WEEK, ON_MONTH, ON_YEAR];

interface ChannelVariant {
  id: number;
  title: string;
}

const dateRangeVariant = ref(dateRangeVariants[0]);
const steamId = ref<undefined | string>();
const steamName = ref<undefined | string>();
const searchValue = ref<undefined | string>();

const loading = computed(() => useChatStore().loading);
const entites = computed(() => useChatStore().entites);

const channelVariants: ChannelVariant[] = [
  { id: 0, title: "GLOBAL" },
  { id: 1, title: "TEAM" },
];

const channelVariant = ref(0);

const getQuery = () => {
  const result = {} as ChatMessageQuery;

  switch (dateRangeVariant.value) {
    case ON_DAY:
      result.from = formatISO(addDays(new Date(), -1));
      result.to = formatISO(new Date());
      break;
    case ON_WEEK:
      result.from = formatISO(addDays(new Date(), -7));
      result.to = formatISO(new Date());
      break;
    case ON_MONTH:
      result.from = formatISO(addMonths(new Date(), -1));
      result.to = formatISO(new Date());
      break;
    case ON_YEAR:
      result.from = formatISO(addYears(new Date(), -1));
      result.to = formatISO(new Date());
      break;
  }

  if (steamId.value) result.steamId = steamId.value;
  if (steamName.value) result.steamName = steamName.value;
  if (channelVariant.value !== undefined) result.channel = channelVariant.value;
  if (searchValue.value !== undefined) result.messageSearch = searchValue.value;

  return result;
};

const fetchData = async () => {
  await useChatStore().fetchData(getQuery());
};

const compactDensityProps = () => {
  return {
    density: "compact",
  };
};

onMounted(() => {
  fetchData();
});
</script>

<style scoped>
.panel_container {
  display: flex;
  flex-direction: column;
}

.panel_count-items {
  margin-top: 12px;
  font-size: 12px;
  text-align: center;
}
</style>
