import { useState } from "react";
import UseTokenHook from "@/hooks/contexts/useTokenHook";
import useAsyncHandler from "./useAsyncHandler";
import { IResponse } from "@/types/IResponse";
import acceptFriend from "@/api/friend/acceptFriend";
import declineFriend from "@/api/friend/declineFriend";

const useNotificationFriendActions = () => {
	const { isLoading, handleAsync } = useAsyncHandler();
	const [status, setStatus] = useState<"none" | "pending" | "added">("none");

	const performAction = async (
		apiFunc: (userId: string, token: string | null) => Promise<IResponse>,
		userId: string,
		newStatus: "none" | "pending" | "added"
	) => {
		const token = await UseTokenHook();
		await handleAsync(
			() => apiFunc(userId, token),
			() => setStatus(newStatus)
		);
	};

	return {
		isLoading,
		status,
		acceptFriend: (userId: string) =>
			performAction(acceptFriend, userId, "pending"),
		declineFriend: (userId: string) =>
			performAction(declineFriend, userId, "none"),
	};
};

export default useNotificationFriendActions;
