export const useToken = () => {
  const STORAGE_KEY = "API-KEY";

  const get = () => {
    return localStorage.getItem(STORAGE_KEY);
  };

  const set = (value: string | null | undefined) => {
    if (value) localStorage.setItem(STORAGE_KEY, value);
    else localStorage.removeItem(STORAGE_KEY);
  };

  const hasToken = () => {
    return !!get();
  };

  return { get, set, hasToken };
};
