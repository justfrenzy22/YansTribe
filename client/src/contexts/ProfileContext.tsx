"use client";

import IProfileContext from "@/types/context/IProfileContext";
import React, { createContext, useState } from "react";

export const ProfileContext = createContext<IProfileContext>({
	user: null,
	profile: null,
	setProfile: () => {},
});

export const ProfileProvider = ({
	children,
	user,
	profile,
	setProfile,
}: {
	children: React.ReactNode;
	user: IProfileContext["user"];
	profile: IProfileContext["profile"];
	setProfile: IProfileContext["setProfile"];
}) => {
	return (
		<ProfileContext.Provider value={{ user, profile, setProfile }}>
			{children}
		</ProfileContext.Provider>
	);
};

export default function ProfileProviderWrapper({
	children,
	user,
	initProfile,
}: {
	children: React.ReactNode;
	user: IProfileContext["user"];
	initProfile: IProfileContext["profile"];
}) {
	const [profile, setProfile] =
		useState<IProfileContext["profile"]>(initProfile);

	return (
		<ProfileProvider user={user} setProfile={setProfile} profile={profile}>
			{children}
		</ProfileProvider>
	);
}
