<script setup lang="ts">
import UserList from "../components/user/UserList.vue";
import {User, userService} from "../services/user-service.ts";
import {onMounted, ref} from "vue";
import CreateUserForm from "../components/user/CreateUserForm.vue";
import UpdateUserForm from "../components/user/UpdateUserForm.vue";

const users = ref<User[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const isUpdating = ref<boolean>(false);
const selectedUser = ref<User | null>(null);

const fetchUsers = async () => {
  loading.value = true;
  try {
    users.value = await userService.getAllUsers();
  } catch (err) {
    error.value = 'Не удалось загрузить пользователей.';
  } finally {
    loading.value = false;
  }
};

const handleDeleteUser = async (userToDelete: User) => {
  try {
    await userService.deleteUser(userToDelete.id);
    users.value = users.value.filter(user => user.id !== userToDelete.id);
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

const onUserCreated = () => fetchUsers();

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
      <UpdateUserForm
          :user="selectedUser"

      />
    </div>
    <div v-else>
      <CreateUserForm
          :on-user-created="onUserCreated"
      />
    </div>
    <UserList
        :users="users"
        :loading="loading"
        :error="error"
        :on-delete="handleDeleteUser"
        :on-start-updating="handleStartUpdating"
        :on-cancel-updating="handleCancelUpdating"
    />
  </div>
</template>

<style scoped>

</style>