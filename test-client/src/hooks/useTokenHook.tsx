"use server";

import { cookies } from "next/headers";

const UseTokenHook = async () => {
	const cookieStore = await cookies();
	const token = cookieStore.get("auth_token")?.value ?? null;
	return token;
};

export default UseTokenHook;
