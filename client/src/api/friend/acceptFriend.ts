"use server";
import { IResponse } from "@/types/IResponse";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const acceptFriend = async (
	user2_id: string,
	cookie: string | null
): Promise<IResponse> =>
	apiService.request<IResponse>({
		endpoint: `/friend/accept/${user2_id}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default acceptFriend;
