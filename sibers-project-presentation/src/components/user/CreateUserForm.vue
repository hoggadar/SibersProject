<script setup lang="ts">
import { ref } from 'vue';
import { UserDto, RoleEnum } from '../../types/user-type';
import { userApi } from '../../api/user-api.ts';

const emit = defineEmits(['on-user-create']);

const newUser = ref<UserDto>({
  firstName: '',
  lastName: '',
  patronymic: '',
  email: '',
  password: '',
  role: RoleEnum.Employee,
});
const message = ref<string | null>(null);
const error = ref<string | null>(null);

const submitForm = async () => {
  try {
    await userApi.createUser(newUser.value);
    emit('on-user-create');
    message.value = 'Пользователь успешно добавлен!';
    error.value = null;
    resetForm();
  } catch (err) {
    error.value = 'Ошибка при добавлении пользователя.';
    message.value = null;
  }
};

const resetForm = () => {
  newUser.value = {
    firstName: '',
    lastName: '',
    patronymic: '',
    email: '',
    password: '',
    role: RoleEnum.Employee,
  };
};
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">Добавить нового пользователя</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="firstName" class="block">Имя:</label>
          <input
            v-model="newUser.firstName"
            type="text"
            id="firstName"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="lastName" class="block">Фамилия:</label>
          <input
            v-model="newUser.lastName"
            type="text"
            id="lastName"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="patronymic" class="block">Отчество:</label>
          <input
            v-model="newUser.patronymic"
            type="text"
            id="patronymic"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>
      </div>

      <div class="flex flex-col">
        <div>
          <label for="email" class="block">Email:</label>
          <input
            v-model="newUser.email"
            type="email"
            id="email"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="password" class="block">Пароль:</label>
          <input
            v-model="newUser.password"
            type="password"
            id="password"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="role" class="block">Роль:</label>
          <select
            v-model="newUser.role"
            id="role"
            class="w-full border border-gray-300 rounded px-3 py-2"
          >
            <option value="Director">Директор</option>
            <option value="ProjectManager">Менеджер проекта</option>
            <option value="Employee">Сотрудник</option>
          </select>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">
          Добавить пользователя
        </button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style scoped></style>
