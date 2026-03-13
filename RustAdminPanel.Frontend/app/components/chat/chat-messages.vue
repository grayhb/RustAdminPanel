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
      <div class="chat-message_container">
        <div class="chat-type" :class="{ global: item.channel === 1 }">
          {{ item.channel === 0 ? "GLOBAL" : "TEAM" }}
        </div>
        <span class="chat-time">
          {{ getDateTime(item.time) }}
        </span>
        <span class="steam-name">
          {{ item.steamName }}
        </span>
        <span class="chat-message">
          {{ item.message }}
        </span>
      </div>
    </template>
  </v-virtual-scroll>
</template>

<script lang="ts" setup>
import { format } from "date-fns";

const virtualScroll = ref();

const items = computed(() => useChatStore().entites);
const loading = computed(() => useChatStore().loading);

const virtualScrollHeight = ref(600);

const getDateTime = (value: number) => {
  return format(new Date(value * 1000), "dd.MM.yyyy HH:mm:ss");
};

const onWindowResize = () => {
  if (virtualScroll.value) {
    const bodyContainer = document.getElementById("body-container");

    if (bodyContainer) {
      virtualScrollHeight.value = bodyContainer?.clientHeight - 20;
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
.chat-message_container {
  display: flex;
  font-size: 14px;
  align-items: center;
  margin-top: 4px;
  margin-bottom: 4px;
}

.chat-time {
  font-size: 12px;
  color: #333;
  white-space: nowrap;
}

.steam-name {
  margin-left: 8px;
  font-weight: 600;
  color: #6164ff;
  margin-right: 8px;
  white-space: nowrap;
}

.chat-type {
  width: 50px;
  text-align: center;
  font-size: 10px;
  background-color: #eee;
  color: #41971e;
  margin-right: 8px;
}

.chat-type.global {
  color: #26184b;
}

.chat-message {
  line-height: 1;
}
</style>
