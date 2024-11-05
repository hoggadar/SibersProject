<script setup lang="ts">

</script>

<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="bg-white p-6 rounded shadow-md w-full max-w-sm">
      <h2 class="text-2xl font-bold mb-6 text-center">Login</h2>
      <form>
        <div class="mb-4">
          <label for="email" class="block text-gray-700">Email:</label>
          <input
              type="email"
              id="email"
              class="block w-full mt-1 px-3 py-2 border rounded"
              required
          />
        </div>
        <div class="mb-4">
          <label for="password" class="block text-gray-700">Password:</label>
          <input
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
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>