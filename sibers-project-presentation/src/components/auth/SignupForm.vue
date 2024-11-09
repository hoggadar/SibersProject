<script setup lang="ts">
import { ref } from 'vue';
import { SignupDto } from '../../types/auth-type';
import { authApi } from '../../api/auth-api';

const formData = ref<SignupDto>({
  firstName: '',
  lastName: '',
  patronymic: '',
  email: '',
  password: '',
});

const errorMessage = ref<string | null>(null);

async function handleSubmit() {
  errorMessage.value = null;
  try {
    const response = await authApi.signup(formData.value);
    alert('Registration successful!');
    console.log(response);
  } catch (error) {
    errorMessage.value = 'An error occurred during registration'; // Ошибка при регистрации
    console.error(error);
  }
}
</script>

<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="bg-white p-6 rounded shadow-md w-full max-w-sm">
      <h2 class="text-2xl font-bold mb-6 text-center">Sign Up</h2>
      <form @submit.prevent="handleSubmit">
        <div class="mb-4">
          <label for="firstName" class="block text-gray-700">First Name:</label>
          <input
            v-model="formData.firstName"
            type="text"
            id="firstName"
            class="block w-full mt-1 px-3 py-2 border rounded"
            required
          />
        </div>
        <div class="mb-4">
          <label for="lastName" class="block text-gray-700">Last Name:</label>
          <input
            v-model="formData.lastName"
            type="text"
            id="lastName"
            class="block w-full mt-1 px-3 py-2 border rounded"
            required
          />
        </div>
        <div class="mb-4">
          <label for="patronymic" class="block text-gray-700"
            >Patronymic:</label
          >
          <input
            v-model="formData.patronymic"
            type="text"
            id="patronymic"
            class="block w-full mt-1 px-3 py-2 border rounded"
            required
          />
        </div>
        <div class="mb-4">
          <label for="email" class="block text-gray-700">Email:</label>
          <input
            v-model="formData.email"
            type="email"
            id="email"
            class="block w-full mt-1 px-3 py-2 border rounded"
            required
          />
        </div>
        <div class="mb-4">
          <label for="password" class="block text-gray-700">Password:</label>
          <input
            v-model="formData.password"
            type="password"
            id="password"
            class="block w-full mt-1 px-3 py-2 border rounded"
            required
          />
        </div>
        <button
          type="submit"
          class="w-full bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-700"
        >
          Sign Up
        </button>
        <div v-if="errorMessage" class="text-red-500 mt-2">
          {{ errorMessage }}
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped></style>
