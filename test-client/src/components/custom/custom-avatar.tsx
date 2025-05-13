import { AvatarSizeType } from "@/types/Avatar/AvatarSizeType";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";

export const CustomAvatar = ({
	username,
	pfp_src,
	size,
}: {
	username: string;
	pfp_src: string;
	size: AvatarSizeType;
}) => {
	return (
		<Avatar className={size}>
			<AvatarImage src={`../${pfp_src}`} alt="User pic" />
			<AvatarFallback>{username.charAt(0)}</AvatarFallback>
		</Avatar>
	);
};
