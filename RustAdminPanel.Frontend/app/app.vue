<template>
  <NuxtLayout>
    <NuxtPage />
  </NuxtLayout>
</template>

<script setup>
import { checkApiKey } from "./services";

useHead({
  htmlAttrs: {
    lang: "ru",
  },
  charset: "utf-8",
  title: "Rust Admin Panel",
  titleTemplate: (title) => title,
  meta: [],
  link: [],
});

const checkToken = async () => {
  if (!(await checkApiKey())) {
    navigateTo("/auth");
  }
};

if (!useToken().hasToken()) {
  navigateTo("/auth");
} else {
  checkToken();
}
</script>
