"use server";
import { IGetCommentsResponse } from "@/types/IResponse";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const getComments = async (
	post_id: string,
	cookie: string | null
): Promise<IGetCommentsResponse> =>
	await apiService.request<IGetCommentsResponse>({
		endpoint: `/comment/get_comments/${post_id}`,
		method: `GET`,
		options: {
			requiresAuth: true,
			cookie: cookie,
		},
	});

export default getComments;
