"use client";
import { ChevronDown, ChevronUp } from "lucide-react";
import { Button } from "../ui/button";
import { Separator } from "@/components/ui/separator";
import HoverProfileCard from "./hover-profile-card";
import { CustomAvatar } from "../custom/custom-avatar";
import { MotionButton, MotionCard } from "../animations/motion-wrapper";
import { useState } from "react";
import { formatDistanceToNow } from "date-fns";
import useProfileFriendActions from "@/hooks/actions/useProfileFriendActions";
import IUserContext from "@/types/context/IProfileContext";
import { useProfile } from "@/hooks/contexts/useProfile";

const ProfileSection = () => {
	const [isOpen, setIsOpen] = useState(false);
	const { profile, setProfile } = useProfile();
	const { isLoading, addFriend, removeFriend, declineFriend, cancelFriend } =
		useProfileFriendActions({
			profile: profile!,
			setProfile,
		});

	if (!profile) return null;

	const handleAction = async (
		action: () => Promise<void>,
		updates: Partial<IUserContext["profile"]>
	) => {
		await action();
		setProfile((prev) => (prev ? { ...prev, ...updates } : prev));
	};

	const renderActionButtons = () => {
		if (profile.is_self) {
			return (
				<MotionButton className="grow">
					<Button
						className="w-full rounded-full cursor-pointer"
						variant="outline"
					>
						Edit profile
					</Button>
				</MotionButton>
			);
		}

		if (!profile.is_friend && profile.is_private) {
			return (
				<MotionButton className="grow">
					<Button
						className="w-full rounded-full cursor-pointer"
						variant="outline"
						disabled={isLoading}
						onClick={() =>
							handleAction(() => addFriend(), {
								is_friend: false,
								request_direction: "sent",
							})
						}
					>
						Add Friend
					</Button>
				</MotionButton>
			);
		}

		switch (profile.request_direction) {
			case "sent":
				return (
					<MotionButton className="grow">
						<Button
							className="w-full rounded-full cursor-pointer"
							variant="outline"
							disabled={isLoading}
							onClick={() =>
								handleAction(() => cancelFriend(), {
									is_friend: false,
									request_direction: null,
								})
							}
						>
							Cancel Request
						</Button>
					</MotionButton>
				);
			case "received":
				return (
					<MotionButton className="grow">
						<Button
							className="w-full rounded-full cursor-pointer"
							variant="outline"
							disabled={isLoading}
							onClick={() =>
								handleAction(() => declineFriend(), {
									request_direction: null,
								})
							}
						>
							Decline Request
						</Button>
					</MotionButton>
				);
			default:
				if (profile.is_friend) {
					return (
						<MotionButton className="grow">
							<Button
								className="w-full rounded-full cursor-pointer"
								variant="outline"
								disabled={isLoading}
								onClick={() =>
									handleAction(() => removeFriend(), {
										is_friend: false,
									})
								}
							>
								Remove Friend
							</Button>
						</MotionButton>
					);
				}
				return (
					<MotionButton className="grow">
						<Button
							className="w-full rounded-full cursor-pointer"
							variant="outline"
							disabled={isLoading}
							onClick={() =>
								handleAction(() => addFriend(), {
									is_friend: false,
									request_direction: "sent",
								})
							}
						>
							Add Friend
						</Button>
					</MotionButton>
				);
		}
	};

	const renderDetails = () => {
		if (!isOpen) {
			return (
				<MotionCard>
					<div
						onClick={() => setIsOpen(true)}
						className="flex py-2 justify-center items-center gap-2 cursor-pointer"
					>
						<p>view more</p>
						<ChevronDown />
					</div>
				</MotionCard>
			);
		}

		return (
			<MotionCard className="py-2">
				<div className="flex flex-col gap-2">
					{profile.bio && (
						<div className="text-muted-foreground text-lg italic">
							{profile.bio}
						</div>
					)}
					<div className="flex flex-col sm:flex-row gap-2">
						{profile.website && (
							<a
								href={profile.website}
								target="_blank"
								className="font-bold hover:underline"
							>
								{profile.website}
							</a>
						)}
						<Separator orientation="vertical" />
						<div className="flex gap-1">
							<span className="text-muted-foreground">Lives in</span>
							<span className="font-bold">{profile.location}</span>
						</div>
						<Separator orientation="vertical" />
						<div className="flex gap-1">
							<span className="text-muted-foreground">Joined</span>
							<span className="font-bold">
								{formatDistanceToNow(new Date(profile.created_at), {
									addSuffix: true,
								})}
							</span>
						</div>
					</div>
				</div>
				<p
					onClick={() => setIsOpen(false)}
					className="mt-4 flex justify-center gap-2 items-center cursor-pointer"
				>
					collapse <ChevronUp />
				</p>
			</MotionCard>
		);
	};

	return (
		<div className="md:space-y-4 space-y-0">
			<div className="mb-4 rounded-3xl bg-secondary/45 border outline shadow-lg">
				<div className="px-6 pt-6 pb-2">
					<div className="flex justify-between items-center">
						<div className="flex flex-col gap-2">
							<div className="flex flex-col items-center">
								<span>{profile.full_name}</span>
								<HoverProfileCard
									username={`@${profile.username}`}
									pfp_src={profile.pfp_src}
									is_private={profile.is_private}
								/>
							</div>
							<span className="text-muted-foreground text-sm">
								{profile.friends_num} friends
							</span>
						</div>
						<CustomAvatar
							pfp_src={profile.pfp_src}
							username={profile.username}
							size="h-20 w-20"
						/>
					</div>

					<div className="flex gap-2 mt-2 w-full">
						{renderActionButtons()}
						{!profile.is_self && !profile.is_private && (
							<MotionButton className="grow">
								<Button
									className="w-full rounded-full cursor-pointer"
									variant="secondary"
								>
									Message
								</Button>
							</MotionButton>
						)}
					</div>

					{profile.is_private && !profile.is_self && !profile.is_friend && (
						<p className="mt-4 italic text-center text-sm text-muted-foreground">
							This profile is private
						</p>
					)}

					{renderDetails()}
				</div>
			</div>
		</div>
	);
};

export default ProfileSection;
