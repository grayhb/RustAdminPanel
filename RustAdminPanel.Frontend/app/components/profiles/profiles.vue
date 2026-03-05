<template>
  <div class="d-flex align-cetner py-2 mb-2">
    <v-btn
      variant="flat"
      color="primary"
      :loading="createProfilesFromLogsLoading"
      @click="onCreateProfilesFromLogs"
    >
      Создать записи из лога
    </v-btn>
    <v-btn
      variant="flat"
      color="primary"
      class="ml-2"
      :loading="refreshSteamDataLoading"
      @click="onRefreshSteamData"
    >
      Обновить данные из стим (тест)
    </v-btn>
  </div>

  <v-progress-linear
    v-if="loading"
    color="primary"
    class="mt-4"
    height="15"
    rounded="lg"
    indeterminate
  ></v-progress-linear>

  <v-virtual-scroll
    v-else
    :items="items"
    :height="virtualScrollHeight"
    ref="virtualScroll"
  >
    <template v-slot:default="{ item }">
      <v-list-item>
        <template v-slot:prepend>
          <v-img
            :src="item.avatar"
            :title="`Профиль Steam - ${item.personaName}`"
            height="32"
            width="32"
            class="profile-avatar"
            @click="onOpenSteamProfile(item.steamId)"
          />
        </template>

        <template v-slot:append>
          <v-chip size="small">
            Последний вход:
            {{ getDateTime(item.lastServerConnectionAt) }}
          </v-chip>
        </template>

        <template v-slot:title>
          <div class="d-flex align-center">
            <div class="profile-persona-name">{{ item.personaName }}</div>
          </div>
          <div v-if="item.steamNames.length > 1" class="profile-steam-names">
            <v-chip
              v-for="steamName in item.steamNames"
              size="x-small"
              :key="steamName"
              label
              class="mr-1"
            >
              {{ steamName }}
            </v-chip>
          </div>
        </template>

        <template v-slot:subtitle>
          <span>Steam ID: {{ item.steamId }}</span>
        </template>
      </v-list-item>
    </template>
  </v-virtual-scroll>
</template>

<script lang="ts" setup>
import { format } from "date-fns";

const virtualScroll = ref();

const items = computed(() => usePlayersStore().profiles);

const loading = computed(() => usePlayersStore().profilesLoading);
const createProfilesFromLogsLoading = computed(
  () => usePlayersStore().createProfilesFromLogsLoading,
);
const refreshSteamDataLoading = computed(
  () => usePlayersStore().refreshSteamDataLoading,
);

const virtualScrollHeight = ref(600);

const getDateTime = (value: string) => {
  return format(value, "dd.MM.yyyy HH:mm:ss");
};

const onOpenSteamProfile = (steamId: string) => {
  window.open(`https://steamcommunity.com/profiles/${steamId}`, "_blank");
};

const onWindowResize = () => {
  if (virtualScroll.value) {
    const bodyContainer = document.getElementById("body-container");

    if (bodyContainer) {
      virtualScrollHeight.value = bodyContainer?.clientHeight - 80;
    }
  }
};

const onCreateProfilesFromLogs = async () => {
  if (createProfilesFromLogsLoading.value) return;
  await usePlayersStore().createProfilesFromLogs();
};

const onRefreshSteamData = async () => {
  if (refreshSteamDataLoading.value) return;
  await usePlayersStore().refreshSteamData();
};

onMounted(() => {
  addEventListener("resize", onWindowResize);
  setTimeout(() => onWindowResize(), 250);
});

onUnmounted(() => {
  removeEventListener("resize", onWindowResize);
});
</script>

<style scoped>
.profile-avatar {
  margin-right: 16px;
  cursor: pointer;
}

.profile-persona-name {
  font-weight: 600;
  line-height: 1;
}

.profile-steam-names {
  font-size: 12px;
  line-height: 1;
}
</style>
