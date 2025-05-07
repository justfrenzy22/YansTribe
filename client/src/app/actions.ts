"use server";

import { cookies } from "next/headers";

export const logout = async () => {
	const cookieStore = await cookies();
	await cookieStore.delete("auth_token");
};

export const saveCookie = async (auth_token: string) => {
	const cookieStore = await cookies();
	await cookieStore.set({
		name: `auth_token`,
		value: auth_token,
		expires: new Date(Date.now() + 1000 * 60 * 60 * 24),
		secure: true,
		path: "/",
	});
};
