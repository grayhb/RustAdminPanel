import { useAPI } from "~/composables";

export const checkApiKey = async (apiKey: string | undefined | null) => {
  if (!apiKey) {
    apiKey = useToken().get();
  }

  if (!apiKey) return false;

  useToken().set(apiKey);

  let result = true;

  await useAPI(`/auth/check`).catch(() => {
    result = false;
    useToken().set(undefined);
  });

  return result;

  //   if (status.value !== "success") {
  //     useToken().set(undefined);
  //     return false;
  //   } else return true;
};
