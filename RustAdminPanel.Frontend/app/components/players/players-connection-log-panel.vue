<template>
  <div class="panel_container">
    <v-form @submit.prevent="fetchData">
      <v-combobox
        label="Период"
        v-model="dateRangeVariant"
        :items="dateRangeVariants"
        variant="solo-filled"
        hide-details
        @update:model-value="fetchData"
        :disabled="loading"
      ></v-combobox>

      <v-text-field
        label="Steam Id"
        v-model="steamId"
        variant="solo-filled"
        hide-details
        :disabled="loading"
        class="mt-4"
        clearable
      ></v-text-field>

      <v-text-field
        label="Steam Name"
        v-model="steamName"
        variant="solo-filled"
        hide-details
        :disabled="loading"
        class="mt-4"
        clearable
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
import type { PlayerConnectionQuery } from "~/types/player.types";

const dateRangeVariants = [ON_DAY, ON_WEEK, ON_MONTH, ON_YEAR];

const dateRangeVariant = ref(dateRangeVariants[0]);
const steamId = ref<undefined | string>();
const steamName = ref<undefined | string>();

const loading = computed(() => usePlayersStore().loading);
const entites = computed(() => usePlayersStore().entites);

const getQuery = () => {
  const result = {} as PlayerConnectionQuery;

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

  return result;
};

const fetchData = async () => {
  await usePlayersStore().fetchData(getQuery());
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
