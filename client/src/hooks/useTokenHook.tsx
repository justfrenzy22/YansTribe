// "use client";
"use server";

import { cookies } from "next/headers";

const UseTokenHook = async () => {
	// const cookie = Cookie.get("auth_token") || null;
	const cookieStore = await cookies();
	const token = cookieStore.get("auth_token")?.value ?? null;
	// future cookie

	return token;
	// return { token, cookie};
};

export default UseTokenHook;
