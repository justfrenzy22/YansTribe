"use server";
import { IPostResponse } from "@/types/IResponse";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const getHomePosts = async (cookie: string | null): Promise<IPostResponse> =>
	apiService.request<IPostResponse>({
		endpoint: `/post/home`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default getHomePosts;
