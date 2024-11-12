<script setup lang="ts">
import { useAuthStore } from '../../store/authStore.ts';

const authStore = useAuthStore();
</script>

<template>
  <header class="flex justify-between items-center p-4 bg-gray-800 text-white">
    <div>
      <h1 class="text-2xl font-bold">Sibers</h1>
    </div>
    <nav>
      <ul class="flex space-x-4">
        <li>
          <router-link
            to="/"
            class="hover:text-blue-400 transition duration-300"
            >Главная</router-link
          >
        </li>
        <li>
          <router-link
            to="/user"
            class="hover:text-blue-400 transition duration-300"
            >Сотрудники</router-link
          >
        </li>
        <li>
          <router-link
            to="/project"
            class="hover:text-blue-400 transition duration-300"
            >Проекты</router-link
          >
        </li>
        <li>
          <router-link
            to="/task"
            class="hover:text-blue-400 transition duration-300"
            >Задачи</router-link
          >
        </li>
      </ul>
    </nav>
    <div class="flex space-x-4">
      <template v-if="authStore.isAuthenticated">
        <router-link to="/login">
          <button
            @click="authStore.logout"
            class="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
          >
            Выйти
          </button>
        </router-link>
      </template>
      <template v-else>
        <router-link to="/login">
          <button
            class="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
          >
            Вход
          </button>
        </router-link>
        <router-link to="/signup">
          <button
            class="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
          >
            Регистрация
          </button>
        </router-link>
      </template>
    </div>
  </header>
</template>

<style scoped></style>
