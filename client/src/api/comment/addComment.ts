"use server";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const addComment = async (
	post_id: string,
	user_id: string,
	content: string,
	cookie: string | null
): Promise<{ message: string; status: number }> =>
	await apiService.request<{ message: string; status: number }>({
		endpoint: `/comment/add`,
		method: `POST`,
		options: {
			body: {
				post_id,
				user_id,
				content,
			},
			requiresAuth: true,
			cookie: cookie,
		},
	});

export default addComment;
