<template>
  <div class="auth_wrapper">
    <div class="auth_card">
      <v-text-field
        v-model="apiKey"
        variant="solo-inverted"
        density="compact"
        hide-details
        @keydown.enter="onSubmit"
      ></v-text-field>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { checkApiKey } from "~/services/auth";

const apiKey = ref("");

const onSubmit = async () => {
  if (!apiKey.value) return;

  if (await checkApiKey(apiKey.value)) navigateTo("/");
};
</script>

<style>
html,
body,
#__nuxt,
.auth_wrapper {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.auth_card {
  width: 300px;
}
</style>
