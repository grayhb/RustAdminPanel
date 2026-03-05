<template>
  <v-app v-if="authed" id="inspire">
    <v-app-bar class="px-3" density="compact" flat>
      <v-avatar
        class="hidden-md-and-up"
        color="grey-darken-1"
        size="32"
      ></v-avatar>

      <v-spacer></v-spacer>

      <v-tabs v-model="tabModel" align-tabs="center" color="grey-darken-2">
        <v-tab v-for="link in links" :key="link" :text="link"></v-tab>
      </v-tabs>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main class="bg-grey-lighten-3">
      <v-container max-width="1200" class="h-100">
        <v-row class="h-100">
          <v-col cols="12" md="3">
            <v-sheet rounded="lg" class="py-2 px-2">
              <players-connection-log-panel v-if="tabModel === 0" />
              <chat-messages-panel v-if="tabModel === 1" />
            </v-sheet>
          </v-col>

          <v-col cols="12" md="9" class="h-100">
            <v-sheet id="body-container" rounded="lg" class="py-2 px-2 h-100">
              <players-connection-log v-if="tabModel === 0" />
              <chat-messages v-if="tabModel === 1" />
            </v-sheet>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup>
const links = ["Player Connections", "Chat"];

const tabModel = ref(0);

const authed = computed(() => useAuthStore().authed);
</script>

<style scoped>
.h-100 {
  height: 100%;
}
</style>
