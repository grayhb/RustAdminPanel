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
      ></v-combobox>

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

import type { PlayerReportQuery } from "~/types/player-report.types";

import { ON_WEEK, ON_MONTH, ON_YEAR, ON_DAY } from "~/constants";

const dateRangeVariants = [ON_DAY, ON_WEEK, ON_MONTH, ON_YEAR];

const dateRangeVariant = ref(dateRangeVariants[1]);

const loading = computed(() => usePlayerReportsStore().playerReportsLoading);
const entites = computed(() => usePlayerReportsStore().playerReports);

const getQuery = () => {
  const result = {} as PlayerReportQuery;

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

  return result;
};

const fetchData = async () => {
  await usePlayerReportsStore().fetchPlayerReports(getQuery());
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
