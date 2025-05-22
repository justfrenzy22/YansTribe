"use server";
import { IResponse } from "@/types/IResponse";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const declineFriend = async (
	user2_id: string,
	cookie: string | null
): Promise<IResponse> =>
	apiService.request<IResponse>({
		endpoint: `/friend/decline/${user2_id}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default declineFriend;
