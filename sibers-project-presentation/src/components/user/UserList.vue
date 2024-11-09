<script setup lang="ts">
import { User } from '../../types/user-type';

interface IProps {
  selectedUserId: number | undefined;
  users: User[];
  loading: boolean;
  error: string | null;
}

const props = defineProps<IProps>();

const emit = defineEmits<{
  (e: 'delete', user: User): void;
  (e: 'start-updating', user: User): void;
  (e: 'cancel-updating'): void;
}>();

const startUpdating = (user: User) => {
  emit('start-updating', user);
};

const cancelUpdating = () => {
  emit('cancel-updating');
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
            <div v-if="selectedUserId === user.id">
              <button
                @click="cancelUpdating()"
                class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              >
                Cancel
              </button>
            </div>
            <div v-else class="flex gap-2">
              <button
                :disabled="!!selectedUserId"
                @click="startUpdating(user)"
                class="bg-yellow-400 hover:bg-yellow-500 text-white font-bold py-2 px-4 rounded"
              >
                Редиктировать
              </button>
              <button
                :disabled="!!selectedUserId"
                @click="() => emit('delete', user)"
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
