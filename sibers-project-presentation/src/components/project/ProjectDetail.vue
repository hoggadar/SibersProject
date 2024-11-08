<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import { userApi } from '../../api/user-api.ts';
import { projectApi } from '../../api/project-api.ts';
import { User } from '../../types/user-type.ts';

const route = useRoute();
const projectId = Number(route.params.projectId);

const assignedUsers = ref<User[]>([]);
const allUsers = ref<User[]>([]);
const selectedUserId = ref<number | null>(null);
const searchQuery = ref<string>('');
const isFocused = ref<boolean>(false);
const showSuggestions = computed(
  () => isFocused.value && filteredUsers.value.length > 0
);

const filteredUsers = computed(() => {
  return allUsers.value.filter((user) => {
    const fullName =
      `${user.lastName} ${user.firstName} ${user.patronymic}`.toLowerCase();
    return fullName.includes(searchQuery.value.toLowerCase());
  });
});

const selectUser = (user: User) => {
  selectedUserId.value = user.id;
  searchQuery.value = `${user.lastName} ${user.firstName} ${user.patronymic}`;
  isFocused.value = false;
};

const handleBlur = () => {
  setTimeout(() => {
    isFocused.value = false;
  }, 100);
};

const addUserToProject = async () => {
  if (selectedUserId.value !== null) {
    await projectApi.addEmployeeToProject({
      EmployeeId: selectedUserId.value,
      ProjectId: projectId,
    });
    assignedUsers.value = await userApi.getUsersByProjectId(projectId);
    searchQuery.value = '';
    isFocused.value = false;
  }
};

const removeUserFromProject = async (userId: number) => {
  await projectApi.removeEmployeeFromProject({
    EmployeeId: userId,
    ProjectId: projectId,
  });
  assignedUsers.value = await userApi.getUsersByProjectId(projectId);
  searchQuery.value = '';
  isFocused.value = false;
};

onMounted(async () => {
  assignedUsers.value = await userApi.getUsersByProjectId(projectId);
  allUsers.value = await userApi.getAllUsers();
});
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">
      Управление пользователями для проекта {{ projectId }}
    </h2>
    <div class="flex gap-2 mb-4">
      <div class="relative w-[320px]">
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
              @click="selectUser(user)"
              class="cursor-pointer hover:bg-gray-200 px-3 py-2"
            >
              {{ user.lastName }} {{ user.firstName }} {{ user.patronymic }}
            </li>
          </ul>
        </div>
      </div>
      <!--      <select-->
      <!--        v-model="selectedUserId"-->
      <!--        class="border border-gray-400 px-2 py-1 rounded"-->
      <!--      >-->
      <!--        <option value="" disabled selected>Выберите пользователя</option>-->
      <!--        <option v-for="user in allUsers" :key="user.id" :value="user.id">-->
      <!--          {{ user.lastName }} {{ user.lastName }} {{ user.patronymic }}-->
      <!--        </option>-->
      <!--      </select>-->
      <button
        @click="addUserToProject"
        class="bg-green-600 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
      >
        Добавить
      </button>
    </div>

    <h3 class="mb-2">Назначенные пользователи</h3>
    <table
      v-if="assignedUsers.length"
      class="bg-white border border-gray-200 shadow-md rounded-lg overflow-hidden"
    >
      <thead class="bg-gray-200">
        <tr>
          <th class="py-2 px-4 text-center">ID</th>
          <th class="py-2 px-4 text-center">Фамилия</th>
          <th class="py-2 px-4 text-center">Имя</th>
          <th class="py-2 px-4 text-center">Отчество</th>
          <th class="py-2 px-4 text-center">Email</th>
          <th class="py-2 px-4 text-center">Роль</th>
          <th class="py-2 px-4 text-center">Действия</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-gray-200">
        <tr
          v-for="user in assignedUsers"
          :key="user.id"
          class="hover:bg-gray-100"
        >
          <td class="py-2 px-4">{{ user.id }}</td>
          <td class="py-2 px-4">{{ user.lastName }}</td>
          <td class="py-2 px-4">{{ user.firstName }}</td>
          <td class="py-2 px-4">{{ user.patronymic }}</td>
          <td class="py-2 px-4">{{ user.email }}</td>
          <td class="py-2 px-4">{{ user.role }}</td>
          <td class="py-2 px-4 flex justify-center">
            <button
              @click="removeUserFromProject(user.id)"
              class="bg-red-600 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
            >
              Удалить
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <div v-else class="mt-4 text-gray-600">Нет назначенных пользователей.</div>
  </div>
</template>

<style scoped></style>
