<script setup lang="ts">
import UserList from '../components/user/UserList.vue';
import { userApi } from '../api/user-api.ts';
import { User } from '../types/user-type';
import { onMounted, ref } from 'vue';
import CreateUserForm from '../components/user/CreateUserForm.vue';
import UpdateUserForm from '../components/user/UpdateUserForm.vue';

const users = ref<User[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const isUpdating = ref<boolean>(false);
const selectedUser = ref<User | null>(null);

const fetchUsers = async () => {
  loading.value = true;
  try {
    users.value = await userApi.getAllUsers();
  } catch (err) {
    error.value = 'Не удалось загрузить пользователей.';
  } finally {
    loading.value = false;
  }
};

const handleDelete = async (userToDelete: User) => {
  try {
    await userApi.deleteUser(userToDelete.id);
    users.value = users.value.filter((user) => user.id !== userToDelete.id);
  } catch (err) {
    error.value = 'Ошибка при удалении пользователя.';
  }
};

const handleStartUpdating = (user: User) => {
  isUpdating.value = true;
  selectedUser.value = user;
};

const handleCancelUpdating = () => {
  isUpdating.value = false;
  selectedUser.value = null;
};

const onUserCreated = () => {
  fetchUsers();
};

const onUserUpdated = () => {
  isUpdating.value = false;
  selectedUser.value = null;
  fetchUsers();
};

onMounted(fetchUsers);
</script>

<template>
  <div class="p-6">
    <div v-if="isUpdating && selectedUser">
      <UpdateUserForm :user="selectedUser" @on-user-update="onUserUpdated" />
    </div>
    <div v-else>
      <CreateUserForm @on-user-create="onUserCreated" />
    </div>
    <UserList
      :users="users"
      :selected-user-id="selectedUser?.id"
      :loading="loading"
      :error="error"
      @delete="handleDelete"
      @startUpdating="handleStartUpdating"
      @cancelUpdating="handleCancelUpdating"
    />
  </div>
</template>

<style scoped></style>
