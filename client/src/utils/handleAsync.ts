// import { toast } from "sonner";
// import { IResponse } from "@/types/IResponse";

// type HandleAsyncParams<T> = {
// 	userId: string;
// 	token: string | null;
// 	action: (userId: string, token: string | null) => Promise<IResponse>;
// 	onSuccess?: () => void;
// 	onError?: () => void;
// 	setLoading?: (val: boolean) => void;
// 	updateState?: () => void;
// };

// const handleAsync = async <T>({
// 	userId,
// 	token,
// 	action,
// 	onSuccess,
// 	onError,
// 	setLoading,
// 	updateState,
// }: HandleAsyncParams<T>) => {
// 	setLoading?.(true);
// 	try {
// 		const res = await action(userId, token);
// 		if (res.status === 200) {
// 			toast.success(res.message);
// 			onSuccess?.();
// 			updateState?.();
// 		} else {
// 			toast.error(res.message);
// 			onError?.();
// 		}
// 	} catch (err) {
// 		toast.error(err instanceof Error ? err.message : "Something went wrong.");
// 		onError?.();
// 	} finally {
// 		setLoading?.(false);
// 	}
// };

// export default handleAsync;
