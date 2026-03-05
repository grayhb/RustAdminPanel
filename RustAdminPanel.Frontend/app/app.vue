<template>
  <NuxtLayout>
    <NuxtPage />
  </NuxtLayout>
</template>

<script setup>
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

const checkAuth = async () => {
  if (!(await useAuthStore().checkAuth())) {
    navigateTo("/auth");
  }
};

if (!useToken().hasToken()) {
  navigateTo("/auth");
} else {
  checkAuth();
}
</script>
