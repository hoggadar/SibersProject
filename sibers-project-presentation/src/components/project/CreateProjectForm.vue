<script setup lang="ts">
import {CreateProjectDto, projectService} from "../../services/project-service.ts";
import {onMounted, ref} from "vue";
import {userService} from "../../services/user-service.ts";

const newProject = ref<CreateProjectDto>({
  title: '',
  customerCompany: '',
  performerCompany: '',
  directorId: 0,
  priority: 0,
});

const users = ref<User[]>([]);
const message = ref<string | null>(null);
const error = ref<string | null>(null);

onMounted(async () => {
  try {
    users.value = await userService.getAllUsers(); // Загружаем пользователей
  } catch (err) {
    console.error(err);
    error.value = 'Ошибка при загрузке пользователей.';
  }
});

const submitForm = async () => {
  try {
    await projectService.createProject(newProject.value);
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
};
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">Добавить новый проект</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="title" class="block">Название проекта:</label>
          <input v-model="newProject.title" type="text" id="title" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="customerCompany" class="block">Заказчик:</label>
          <input v-model="newProject.customerCompany" type="text" id="customerCompany" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="performerCompany" class="block">Исполнитель:</label>
          <input v-model="newProject.performerCompany" type="text" id="performerCompany" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>
      </div>

      <div class="flex flex-col">
        <div>
          <label for="directorId" class="block">Директор:</label>
          <select v-model.number="newProject.directorId" id="directorId" class="w-full border border-gray-300 rounded px-3 py-2">
            <option value="" disabled selected>Выберите директора</option>
            <option v-for="user in users" :key="user.id" :value="user.id">
              {{ user.lastName }} {{ user.firstName }} {{ user.patronymic }}
            </option>
          </select>
        </div>

        <div>
          <label for="priority" class="block">Приоритет:</label>
          <select v-model.number="newProject.priority" id="priority" class="w-full border border-gray-300 rounded px-3 py-2">
            <option value="1">1 (Низкий)</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5 (Высокий)</option>
          </select>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">Добавить проект</button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style scoped>

</style>