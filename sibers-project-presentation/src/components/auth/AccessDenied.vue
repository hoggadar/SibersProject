<script setup lang="ts">
import { useRouter } from 'vue-router';

const router = useRouter();
const goToHome = () => {
  router.push('/');
};
</script>

<template>
  <div
    class="flex flex-col flex-grow justify-center items-center bg-red-100 text-red-800 text-center"
  >
    <h1 class="text-5xl font-bold mb-4">Доступ запрещен</h1>
    <p class="text-lg mb-6">
      Извините, у вас нет разрешения на доступ к этой странице.
    </p>
    <button
      @click="goToHome"
      class="px-4 py-2 bg-red-600 text-white font-semibold rounded-lg shadow-lg hover:bg-red-700 transition duration-300"
    >
      На главную
    </button>
  </div>
</template>

<style scoped></style>
