// hooks/useAsyncHandler.ts
import { useState } from "react";
import { toast } from "sonner";
import { IResponse } from "@/types/IResponse";

const useAsyncHandler = () => {
	const [isLoading, setIsLoading] = useState(false);

	const handleAsync = async <T extends IResponse>(
		callback: () => Promise<T>,
		onSuccess?: (res: T) => void,
		onError?: (res: T) => void
	) => {
		setIsLoading(true);
		try {
			const res = await callback();
			if (res.status === 200) {
				toast.success(res.message);
				onSuccess?.(res);
			} else {
				toast.error(res.message);
				onError?.(res);
			}
		} catch (err) {
			toast.error(err instanceof Error ? err.message : "Something went wrong.");
		} finally {
			setIsLoading(false);
		}
	};

	return { isLoading, handleAsync };
};

export default useAsyncHandler;
