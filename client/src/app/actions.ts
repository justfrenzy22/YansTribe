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

export const retrieveSessionToken = async (
	showLoading: () => void,
	hideLoading: () => void
): Promise<string | null> => {
	try {
		showLoading();
		const sessionToken = sessionStorage.getItem("session_token");
		return sessionToken || null;
	} finally {
		hideLoading(); // Hide the loading animation
	}
};
export const parseSessionToken = async (
	sessionToken: string
): Promise<Response> => {
	return fetch("/api/parse-token", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify({ token: sessionToken }),
	});
};
