const getBaseUrl = () => {
  if (location.origin.indexOf("localhost") > -1)
    return "http://localhost:5284/";

  return location.origin + "/api/";
};

export function useAPI(
  url: string,
  fetchMethod:
    | "GET"
    | "HEAD"
    | "PATCH"
    | "POST"
    | "PUT"
    | "DELETE"
    | "CONNECT"
    | "OPTIONS"
    | "TRACE"
    | "get"
    | "head"
    | "patch"
    | "post"
    | "put"
    | "delete"
    | "connect"
    | "options"
    | "trace"
    | undefined = "GET",
  payload: object | string | undefined = undefined,
) {
  const apiKey = useToken().get();

  return $fetch(url, {
    baseURL: getBaseUrl(),
    method: fetchMethod,
    body: payload,
    headers: {
      "X-API-Key": apiKey,
      Accept: "application/json",
      "Content-type": "application/json",
    },
  });
}
