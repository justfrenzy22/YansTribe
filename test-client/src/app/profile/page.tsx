"use client";

import FriendsCard from "@/components/custom/friends-card";
import { Navbar } from "@/components/custom/navbar";
import PostFeed from "@/components/custom/post-feed";
import ProfileSection from "@/components/custom/profile-section";
import { useState } from "react";
const ProfilePage = () => {
	const [isOpen, setIsOpen] = useState(false);

	return (
		<>
			<Navbar />
			<main className="container mx-auto pt-16 md:px-4 px-2">
				<div className="flex gap-4">
					{/* Left Sidebar - hidden on mobile */}
					<div className="hidden md:block w-1/4 lg:w-1/5">
						<div className="sticky top-20">
							{/* <Sidebar /> */}
							sidebar
						</div>
					</div>

					{/* Main Content */}
					<div className="w-full md:w-2/4 lg:w-3/5 space-y-0">
						<ProfileSection
							isOpen={isOpen}
							setIsOpen={setIsOpen}
							fullName="Petar Yankov"
							username="@justfrenzy22"
							friends_num={20}
							website="https://crackflix.site"
							location="Eindhoven"
							created_at="December 2020"
							pfp_src="/img/pfp.png"
							is_private={false}
							bio={`Digital creator | Photography enthusiast | Travel lover`}
						/>
						<p>Stories</p>
						<FriendsCard />
						<PostFeed />
					</div>

					{/* Right Sidebar - hidden on mobile */}
					<div className="hidden md:block w-1/4 lg:w-1/5">
						<div className="sticky top-20">
							{/* <RightSidebar /> */}
							right bar
						</div>
					</div>
				</div>
			</main>
		</>
	);
};

export default ProfilePage;
