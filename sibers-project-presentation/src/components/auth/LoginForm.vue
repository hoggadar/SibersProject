<script setup lang="ts">
import { authApi } from '../../api/auth-api.ts';
import { ref } from 'vue';
import { LoginDto } from '../../types/auth-type.ts';
import { useRouter } from 'vue-router';

const formData = ref<LoginDto>({
  email: '',
  password: '',
});
const router = useRouter();
const errorMessage = ref<string | null>(null);

async function handleSubmit() {
  errorMessage.value = null;
  try {
    const response = await authApi.login(formData.value);
    alert('Login successful!');
    console.log(response);
    await router.push({ name: 'home' });
  } catch (error) {
    errorMessage.value = 'An error occurred during login';
    console.error(error);
  }
}
</script>

<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="bg-white p-6 rounded shadow-md w-full max-w-sm">
      <h2 class="text-2xl font-bold mb-6 text-center">Login</h2>
      <form @submit.prevent="handleSubmit">
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
          Log In
        </button>
        <div v-if="errorMessage" class="text-red-500 mt-2">
          {{ errorMessage }}
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped></style>
