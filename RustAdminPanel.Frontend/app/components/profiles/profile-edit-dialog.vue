<template>
  <v-dialog v-model="dialogProxy" max-width="500px" persistent>
    <v-card>
      <v-card-title class="text-h6"> Редактировать заметку </v-card-title>

      <v-card-text>
        <v-textarea
          v-model="localNote"
          label="Текст заметки"
          placeholder="Введите текст..."
          rows="4"
          auto-grow
          variant="outlined"
          density="compact"
          color="primary"
        ></v-textarea>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn color="grey-darken-1" variant="tonal" @click="handleCancel">
          Отмена
        </v-btn>

        <v-btn color="primary" @click="handleSave"> Сохранить </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts" setup>
import { ref, watch, type PropType } from "vue";
import type { PlayerProfile, ProfileUpdateDto } from "~/types/player.types";

const props = defineProps({
  modelValue: {
    type: Boolean,
    default: false,
  },
  item: {
    type: Object as PropType<PlayerProfile>,
    default: undefined,
  },
});

const emit = defineEmits(["update:modelValue", "save", "cancel"]);

const localNote = ref("");

const dialogProxy = ref(props.modelValue);

watch(
  () => props.item,
  (newVal) => {
    localNote.value = newVal?.note ?? "";
  },
  { immediate: true },
);

watch(
  () => props.modelValue,
  (newVal) => {
    dialogProxy.value = newVal;
    if (newVal) {
      localNote.value = props.item?.note ?? "";
    }
  },
);

watch(dialogProxy, (newVal) => {
  emit("update:modelValue", newVal);
});

const handleCancel = () => {
  emit("cancel");
  dialogProxy.value = false;
};

// Логика кнопки "Сохранить"
const handleSave = () => {
  if (!props.item) return;

  emit("save", {
    id: props.item.id,
    note: localNote.value,
  } as ProfileUpdateDto);

  dialogProxy.value = false;
};
</script>
