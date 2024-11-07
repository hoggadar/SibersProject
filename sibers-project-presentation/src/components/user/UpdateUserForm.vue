<script setup lang="ts">
import {ref, watch} from 'vue';
import {User, RoleEnum, userService, UserDto} from '../../services/user-service.ts';

const props = defineProps<{
  user: User;
  onUserUpdate: () => void;
}>();

const updatedUser = ref<UserDto>({
  firstName: '',
  lastName: '',
  patronymic: '',
  email: '',
  password: '',
  role: RoleEnum.Employee,
});

const message = ref<string | null>(null);
const error = ref<string | null>(null);

watch(() => props.user, (newValue) => {
  updatedUser.value = {
    firstName: newValue.firstName,
    lastName: newValue.lastName,
    patronymic: newValue.patronymic,
    email: newValue.email,
    password: newValue.password,
    role: newValue.role || RoleEnum.Employee,
  };
}, { immediate: true });

const submitForm = async () => {
  try {
    await userService.updateUser(props.user.id, updatedUser.value);
    props.onUserUpdate();
    message.value = 'Пользователь успешно обновлен!';
    error.value = null;
    resetForm();
  } catch (err) {
    error.value = 'Ошибка при обновлении пользователя.';
    message.value = null;
  }
};

const resetForm = () => {
  updatedUser.value = {
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
    <h2 class="text-2xl font-bold mb-4">Редактировать пользователя {{ props.user.id }}</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="firstName" class="block">Имя:</label>
          <input v-model="updatedUser.firstName" type="text" id="firstName" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="lastName" class="block">Фамилия:</label>
          <input v-model="updatedUser.lastName" type="text" id="lastName" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="patronymic" class="block">Отчество:</label>
          <input v-model="updatedUser.patronymic" type="text" id="patronymic" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>
      </div>

      <div class="flex flex-col">
        <div>
          <label for="email" class="block">Email:</label>
          <input v-model="updatedUser.email" type="email" id="email" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="password" class="block">Пароль:</label>
          <input v-model="updatedUser.password" type="password" id="password" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="role" class="block">Роль:</label>
          <select v-model="updatedUser.role" id="role" class="w-full border border-gray-300 rounded px-3 py-2">
            <option v-for="role in Object.values(RoleEnum)" :key="role" :value="role">
              {{ role }}
            </option>
          </select>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">Сохранить изменения</button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style scoped>

</style>