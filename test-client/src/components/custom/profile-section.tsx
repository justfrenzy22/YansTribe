import { MotionButton, MotionCard } from "@/animations/motion-wrapper";
import { ChevronDown, ChevronUp } from "lucide-react";
import { CustomAvatar } from "./custom-avatar";
import { Button } from "../ui/button";
import { Separator } from "@/components/ui/separator";
import HoverProfileCard from "./hover-profile-card";

const ProfileSection = ({
	isOpen,
	setIsOpen,
	fullName,
	username,
	friends_num,
	website,
	location,
	created_at,
	bio,
	pfp_src,
	is_private,
}: {
	isOpen: boolean;
	setIsOpen: React.Dispatch<React.SetStateAction<boolean>>;
	fullName: string;
	username: string;
	friends_num: number;
	website: string;
	location: string;
	created_at: string;
	bio: string;
	pfp_src: string;
	is_private: boolean;
}) => {
	return (
		<div className="md:space-y-4 space-y-0">
			<div className="mb-4 justify-center md:pb-0 rounded-3xl bg-secondary/45 dark:bg-secondary/45 border outline shadow-lg">
				<div>
					<div className="px-6 pt-6 pb-2">
						<div className="flex flex-row items-center justify-between w-full">
							<div className="flex flex-col gap-2">
								<div className="flex flex-col">
									<span>{fullName}</span>
									<HoverProfileCard
										username={username}
										pfp_src={pfp_src}
										is_private={is_private}
									/>
								</div>
								<div>
									<span className="text-muted-foreground text-sm">
										{friends_num} friends
									</span>
								</div>
							</div>
							<CustomAvatar pfp_src="asd" username="asdasd" size="h-20 w-20" />
						</div>
						<div className="flex flex-row gap-2 w-full p-2 mt-2">
							<MotionButton className="grow">
								<Button className="w-full rounded-full cursor-pointer">
									Add Friend
								</Button>
							</MotionButton>
							<MotionButton className="grow">
								<Button
									className="w-full rounded-full cursor-pointer"
									variant={`secondary`}
								>
									Message
								</Button>
							</MotionButton>
						</div>
						{!isOpen && (
							<MotionCard>
								<div
									onClick={() => setIsOpen(!isOpen)}
									className="flex py-2 w-full flex-row gap-2 justify-center items-center text-center "
								>
									<p className="cursor-pointer">view more</p>
									<ChevronDown />
								</div>
							</MotionCard>
						)}

						{isOpen && (
							<MotionCard className="py-2">
								<div>
									<div className="flex flex-col gap-2">
										<div className="text-muted-foreground text-lg italic">{bio}</div>
										<div className="flex flex-col sm:flex-row gap-2">
											<div>
												<a
													target="_blank"
													href={website}
													className="font-bold hover:underline"
												>
													{website}
												</a>
											</div>
											<Separator orientation="vertical" />
											<div className="flex flex-row gap-1">
												<span className="text-muted-foreground">Lives in </span>
												<span className="font-bold">{location}</span>
											</div>
											<Separator orientation="vertical" />
											<div className="flex flex-row gap-1">
												<span className="text-muted-foreground">Joined</span>
												<span className="font-bold">{created_at}</span>
											</div>
										</div>
									</div>

									<p
										onClick={() => setIsOpen(!isOpen)}
										className="grow cursor-pointer mt-4 flex flex-row gap-2 items-center justify-center w-full"
									>
										collapse
										<ChevronUp />
									</p>
								</div>
							</MotionCard>
						)}
					</div>
				</div>
			</div>
		</div>
	);
};

export default ProfileSection;
