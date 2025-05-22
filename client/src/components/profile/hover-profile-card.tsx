import { redirect } from "next/navigation";
import {
	HoverCard,
	HoverCardContent,
	HoverCardTrigger,
} from "../ui/hover-card";
import { CustomAvatar } from "../custom/custom-avatar";
// import { CustomAvatar } from "./custom-avatar";

const HoverProfileCard = ({
	username,
	pfp_src,
	is_private,
}: {
	username: string;
	pfp_src: string;
	is_private: boolean;
}) => {
	return (
		<HoverCard>
			<HoverCardTrigger
				onClick={() => redirect(`/${username}`)}
				className="text-blue-500 hover:underline underline-offset-2 cursor-pointer"
			>
				{username}
			</HoverCardTrigger>
			<HoverCardContent className="w-full">
				<div className="flex flex-row gap-2 p-2">
					<CustomAvatar pfp_src={pfp_src} size="h-9 w-9" username={username} />
					<div>
						<p>{username}</p>
						{is_private ? (
							<p className="text-muted-foreground">This account is private</p>
						) : (
							<div className="flex flex-row gap-2">
								{Array.from({ length: 3 }).map((_, i) => (
									<div key={i} className="gap-2 w-full">
										<img
											src="https://my.alfred.edu/zoom/_images/foster-lake.jpg"
											alt="pfp"
											className="w-16 h-16 rounded-lg"
										/>
										<p>John Doe</p>
									</div>
								))}
							</div>
						)}
					</div>
				</div>
			</HoverCardContent>
		</HoverCard>
	);
};

export default HoverProfileCard;
