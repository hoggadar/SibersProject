<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import { User } from '../../types/user-type.ts';
import { CreateProjectDto } from '../../types/project-type';
import { userApi } from '../../api/user-api.ts';
import { projectApi } from '../../api/project-api.ts';

const newProject = ref<CreateProjectDto>({
  title: '',
  customerCompany: '',
  performerCompany: '',
  directorId: 0,
  priority: 0,
});

const users = ref<User[]>([]);
const searchQuery = ref<string>('');
const message = ref<string | null>(null);
const error = ref<string | null>(null);
const isFocused = ref<boolean>(false);
const showSuggestions = computed(
  () => isFocused.value && filteredUsers.value.length > 0
);

const filteredUsers = computed(() => {
  return users.value.filter((user) => {
    const fullName =
      `${user.lastName} ${user.firstName} ${user.patronymic}`.toLowerCase();
    return fullName.includes(searchQuery.value.toLowerCase());
  });
});

const selectDirector = (user: User) => {
  newProject.value.directorId = user.id;
  searchQuery.value = `${user.lastName} ${user.firstName} ${user.patronymic}`;
  isFocused.value = false;
};

const handleBlur = () => {
  setTimeout(() => {
    isFocused.value = false;
  }, 100);
};

const submitForm = async () => {
  try {
    await projectApi.createProject(newProject.value);
    message.value = 'Проект успешно добавлен!';
    error.value = null;
    resetForm();
  } catch (err) {
    error.value = 'Ошибка при добавлении проекта.';
    message.value = null;
  }
};

const resetForm = () => {
  newProject.value = {
    title: '',
    customerCompany: '',
    performerCompany: '',
    directorId: 0,
    priority: 1,
  };
  searchQuery.value = '';
  isFocused.value = false;
};

onMounted(async () => {
  try {
    users.value = await userApi.getAllUsers();
  } catch (err) {
    console.error(err);
    error.value = 'Ошибка при загрузке пользователей.';
  }
});
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">Добавить новый проект</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="title" class="block">Название проекта:</label>
          <input
            v-model="newProject.title"
            type="text"
            id="title"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="customerCompany" class="block">Заказчик:</label>
          <input
            v-model="newProject.customerCompany"
            type="text"
            id="customerCompany"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="performerCompany" class="block">Исполнитель:</label>
          <input
            v-model="newProject.performerCompany"
            type="text"
            id="performerCompany"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>
      </div>

      <div class="flex flex-col relative">
        <div>
          <label for="directorId" class="block">Директор:</label>
          <div>
            <input
              v-model="searchQuery"
              type="text"
              placeholder="Начните вводить ФИО..."
              @focus="isFocused = true"
              @blur="handleBlur"
              class="w-full border border-gray-300 rounded px-3 py-2 mb-2"
            />
            <ul
              v-if="showSuggestions"
              class="absolute left-0 w-full border border-gray-300 rounded mt-1 bg-white z-10"
            >
              <li
                v-for="user in filteredUsers"
                :key="user.id"
                @click="selectDirector(user)"
                class="cursor-pointer hover:bg-gray-200 px-3 py-2"
              >
                {{ user.lastName }} {{ user.firstName }} {{ user.patronymic }}
              </li>
            </ul>
          </div>
        </div>

        <div>
          <label for="priority" class="block">Приоритет:</label>
          <select
            v-model.number="newProject.priority"
            id="priority"
            class="w-full border border-gray-300 rounded px-3 py-2"
          >
            <option value="1">1 (Низкий)</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5 (Высокий)</option>
          </select>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">
          Добавить проект
        </button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style scoped></style>
