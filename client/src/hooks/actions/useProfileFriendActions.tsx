import addFriendAsync from "@/api/friend/addFriend";
import declineFriendAsync from "@/api/friend/declineFriend";
import removeFriendAsync from "@/api/friend/removeFriend";
import { IResponse } from "@/types/IResponse";
import IProfileContext from "@/types/context/IProfileContext";
import UseTokenHook from "../contexts/useTokenHook";
import useAsyncHandler from "./useAsyncHandler";

const useProfileFriendActions = ({
	profile,
	setProfile,
}: {
	profile: IProfileContext[`profile`];
	setProfile?: IProfileContext[`setProfile`];
}) => {
	const { isLoading, handleAsync } = useAsyncHandler();

	const handleAction = async ({
		apiFunc,
		is_friend,
		friend_status,
	}: {
		apiFunc: (userId: string, token: string) => Promise<IResponse>;
		is_friend: boolean;
		friend_status: string;
	}) => {
		if (!profile) return;
		const token = await UseTokenHook();
		await handleAsync(
			() => apiFunc(profile.user_id, token as string),
			() =>
				setProfile?.((prev) =>
					prev
						? { ...prev!, is_friend: is_friend, friend_status: friend_status }
						: prev
				)
		);
	};

	// const addFriend = (
	// 	userId: string,
	// 	setProfile?: IProfileContext["setProfile"]
	// ) =>
	// 	handle(userId, addFriendAsync, setProfile, (prev) => ({
	// 		...prev!,
	// 		is_friend: false,
	// 		friend_status: "pending",
	// 	}));

	// const removeFriend = (
	// 	userId: string,
	// 	setProfile?: IProfileContext["setProfile"]
	// ) =>
	// 	handle(userId, removeFriendAsync, setProfile, (prev) => ({
	// 		...prev!,
	// 		is_friend: false,
	// 		friend_status: "none",
	// 	}));

	// const declineFriend = (
	// 	userId: string,
	// 	setProfile?: IProfileContext["setProfile"]
	// ) =>
	// 	handle(userId, declineFriendAsync, setProfile, (prev) => ({
	// 		...prev!,
	// 		is_friend: false,
	// 		friend_status: "none",
	// 	}));

	// const cancelFriend = (
	// 	userId: string,
	// 	setProfile?: IProfileContext["setProfile"]
	// ) =>
	// 	handle(userId, declineFriendAsync, setProfile, (prev) => ({
	// 		...prev!,
	// 		is_friend: false,
	// 		friend_status: "none",
	// 	}));

	const addFriend = async () =>
		handleAction({
			apiFunc: addFriendAsync,
			is_friend: false,
			friend_status: "pending",
		});
	const removeFriend = () =>
		handleAction({
			apiFunc: removeFriendAsync,
			is_friend: false,
			friend_status: "none",
		});
	const declineFriend = () =>
		handleAction({
			apiFunc: declineFriendAsync,
			is_friend: false,
			friend_status: "none",
		});
	const cancelFriend = () =>
		handleAction({
			apiFunc: declineFriendAsync,
			is_friend: false,
			friend_status: "none",
		});

	return {
		isLoading,
		addFriend,
		removeFriend,
		declineFriend,
		cancelFriend,
	};
};

export default useProfileFriendActions;
