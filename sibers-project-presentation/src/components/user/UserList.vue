<script setup lang="ts">
import { User } from '../../types/user-type';
import { ref } from 'vue';

const props = defineProps<{
  users: User[];
  loading: boolean;
  error: string | null;
  onDelete: (user: User) => void;
  onStartUpdating: (user: User) => void;
  onCancelUpdating: () => void;
}>();

const updatingUserId = ref<number | null>(null);

const startUpdating = (user: User) => {
  updatingUserId.value = user.id;
  props.onStartUpdating(user);
};

const cancelUpdating = () => {
  updatingUserId.value = null;
  props.onCancelUpdating();
};
</script>

<template>
  <div>
    <div v-if="props.loading" class="text-blue-500">Загрузка...</div>
    <div v-if="props.error" class="text-red-500">{{ props.error }}</div>

    <table
      v-if="!props.loading && props.users.length"
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
          v-for="user in props.users"
          :key="user.id"
          class="hover:bg-gray-100"
        >
          <td class="py-2 px-4">{{ user.id }}</td>
          <td class="py-2 px-4">{{ user.lastName }}</td>
          <td class="py-2 px-4">{{ user.firstName }}</td>
          <td class="py-2 px-4">{{ user.patronymic }}</td>
          <td class="py-2 px-4">{{ user.email }}</td>
          <td class="py-2 px-4">{{ user.role }}</td>
          <td class="py-2 px-4 w-[274px] flex justify-center">
            <div v-if="updatingUserId === user.id">
              <button
                @click="cancelUpdating()"
                class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              >
                Cancel
              </button>
            </div>
            <div v-else class="flex gap-2">
              <button
                :disabled="updatingUserId !== null"
                @click="startUpdating(user)"
                class="bg-yellow-400 hover:bg-yellow-500 text-white font-bold py-2 px-4 rounded"
              >
                Редиктировать
              </button>
              <button
                :disabled="updatingUserId !== null"
                @click="props.onDelete(user)"
                class="bg-red-600 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
              >
                Удалить
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <div
      v-else-if="!props.loading && !props.users.length"
      class="mt-4 text-gray-600"
    >
      Нет пользователей для отображения.
    </div>
  </div>
</template>

<style scoped></style>
