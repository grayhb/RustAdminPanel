<template>
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
      <v-list-item v-if="item.dataParsed">
        <div class="d-flex align-center">
          <div class="info-container">
            <div class="mx-4 text-caption">
              {{ getDateTime(item.createdAt) }}
            </div>
            <div class="ml-4">
              <div class="report-subject">{{ item.dataParsed.Subject }}</div>
              <div class="report-message">{{ item.dataParsed.Message }}</div>
            </div>
          </div>
          <div class="names-container">
            <div class="user-container">
              <div
                class="steam-name"
                @click="onOpenSteamProfile(item.playerId)"
              >
                {{ item.playerName }}
              </div>
              <div class="steam-id">{{ item.playerId }}</div>
            </div>
            <v-icon color="error" class="mx-4">mdi-arrow-right-thick</v-icon>
            <div class="user-container">
              <div
                class="steam-name target"
                @click="onOpenSteamProfile(item.dataParsed.TargetId)"
              >
                {{ item.dataParsed.TargetName ?? "-" }}
              </div>
              <div class="steam-id target">
                {{ item.dataParsed.TargetId ?? "-" }}
              </div>
            </div>
          </div>
        </div>

        <v-divider class="mt-2" />
      </v-list-item>
    </template>
  </v-virtual-scroll>
</template>

<script lang="ts" setup>
import { format } from "date-fns";

const virtualScroll = ref();

const items = computed(() => usePlayerReportsStore().playerReports);

const loading = computed(() => usePlayerReportsStore().playerReportsLoading);

const virtualScrollHeight = ref(600);

const getDateTime = (value: string) => {
  return format(value, "dd.MM.yyyy HH:mm:ss");
};

const onOpenSteamProfile = (steamId: string | undefined) => {
  if (!steamId) return;
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

watch(items, () => {
  onWindowResize();
});

onMounted(() => {
  addEventListener("resize", onWindowResize);
  setTimeout(() => onWindowResize(), 250);
});

onUnmounted(() => {
  removeEventListener("resize", onWindowResize);
});
</script>

<style scoped>
.steam-id {
  font-size: 12px;
  color: #666;
}

.steam-name {
  font-weight: 600;
  font-size: 14px;
  color: #222;
  cursor: pointer;
}

.report-subject {
  line-height: 1;
  font-size: 12px;
  font-weight: 600;
}

.report-message {
  font-size: 14px;
}

.target {
  color: #ff3f3f;
}

.info-container {
  display: flex;
  align-items: center;
  flex-grow: 1;
}

.names-container {
  display: flex;
  align-items: center;
}

.user-container {
  width: 150px;
  text-overflow: clip;
  overflow: hidden;
}
</style>
