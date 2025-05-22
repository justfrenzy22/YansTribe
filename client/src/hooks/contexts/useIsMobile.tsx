import { useEffect, useState } from "react";

const useIsMobile = (userAgent: string) => {
	const [isMobile, setIsMobile] = useState<boolean>(false);

	useEffect(() => {
		if (typeof userAgent === "string") {
			const mobile = Boolean(
				userAgent.match(
					/Android|BlackBerry|iPhone|iPad|iPod|Opera Mini|IEMobile/i
				)
			);
			setIsMobile(mobile);
		}
	}, [userAgent]);

	return { isMobile };
};

export default useIsMobile;
