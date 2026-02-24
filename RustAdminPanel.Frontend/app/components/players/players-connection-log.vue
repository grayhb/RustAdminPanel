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
      <v-list-item
        :subtitle="`Steam ID: ${item.steamId}`"
        :title="item.steamName"
      >
        <template v-slot:prepend>
          <v-btn
            icon="mdi-steam"
            variant="text"
            density="compact"
            class="mr-4"
            title="Профиль Steam"
            @click="onOpenSteamProfile(item.steamId)"
          ></v-btn>
        </template>

        <template v-slot:append>
          <v-chip size="small">
            {{ getDateTime(item.connectionTimestamp) }}
          </v-chip>
        </template>
      </v-list-item>
    </template>
  </v-virtual-scroll>
</template>

<script lang="ts" setup>
import { format } from "date-fns";

const virtualScroll = ref();

const items = computed(() => usePlayersStore().entites);
const loading = computed(() => usePlayersStore().loading);

const virtualScrollHeight = ref(600);

const getDateTime = (value: number) => {
  return format(new Date(value * 1000), "dd.MM.yyyy HH:mm:ss");
};

const onOpenSteamProfile = (steamId: string) => {
  window.open(`https://steamcommunity.com/profiles/${steamId}`, "_blank");
};

const onWindowResize = () => {
  if (virtualScroll.value) {
    const bodyContainer = document.getElementById("body-container");

    if (bodyContainer) {
      virtualScrollHeight.value = bodyContainer?.clientHeight - 20;
    }
  }
};

onMounted(() => {
  addEventListener("resize", onWindowResize);
  setTimeout(() => onWindowResize(), 250);
});

onUnmounted(() => {
  removeEventListener("resize", onWindowResize);
});
</script>
