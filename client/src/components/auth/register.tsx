import { IRegisterForm } from "@/types/IRegisterForm";
import { step } from "@/types/stepType";
import { AnimatePresence, motion } from "framer-motion";
import React, { useEffect, useRef, useState } from "react";
import ProgressSteps from "./progress-steps";
import AccountStep from "./account-step";
import { IRegisterErrors } from "@/types/IRegisterErrors";
import { Button } from "../ui/button";
import { ChevronLeft, ChevronRight, Loader2 } from "lucide-react";
import ProfileStep from "./profile-step";
import ReviewStep from "./review-step";
import { useTheme } from "next-themes";

const Register = ({
	setLogin,
}: {
	setLogin: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const { theme } = useTheme();
	const [isLoading, setLoading] = useState<boolean>();
	const [currStep, setCurrStep] = useState<step>("account");
	const [formData, setFormData] = useState<IRegisterForm>({
		username: ``,
		email: ``,
		password: ``,
		confirmPassword: ``,

		fullName: ``,
		bio: ``,
		location: ``,
		website: ``,

		agreeTerms: false,
	});
	const [errors, setErrors] = useState<IRegisterErrors>({});
	const usernameRef = useRef<HTMLInputElement | null>(null);
	const fullNameRef = useRef<HTMLInputElement | null>(null);
	const [showPassord, setShowPassword] = useState<boolean>(false);
	const [showConfirmPassword, setShowConfirmPassword] =
		useState<boolean>(false);

	useEffect(() => {
		if (currStep === `account`) {
			if (usernameRef.current) {
				usernameRef.current.focus();
			}
		} else if (currStep === `profile`) {
			if (fullNameRef.current) {
				fullNameRef.current.focus();
			}
		}
	}, [currStep]);

	const handleNext = (): void => {
		if (currStep === `account` && validateAccountStep()) {
			setCurrStep(`profile`);
		} else if (currStep === `profile` && validateProfileStep()) {
			setCurrStep(`review`);
		}
	};

	const validateAccountStep = (): boolean => {
		if (!formData.username || !formData.email || !formData.password) {
			setErrors({ form: `Please fill in all fields` });
			return false;
		}
		const pattern = /[a-z0-9]+@[a-z0-9]+\.[a-z]{2,3}/;
		if (!pattern.test(formData.email)) {
			setErrors({ email: `Wrong email format` });
			return false;
		}

		return true;
	};

	const validateProfileStep = (): boolean => {
		if (
			!formData.fullName ||
			!formData.bio ||
			!formData.location ||
			!formData.website
		) {
			setErrors({ form: `Please fill in all fields` });
			return false;
		}
		return true;
	};

	const handleBack = (): void => {
		if (currStep === `profile`) {
			setCurrStep(`account`);
		} else if (currStep === `review`) {
			setCurrStep(`profile`);
		}
	};

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();

		if (formData.agreeTerms !== true) {
			setErrors({ agreeTerms: `You must agree to the terms and conditions` });
			return;
		}

		try {
			setLoading(true);
			const res = await fetch("/api/auth/register", {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify(formData),
			})
				.then((res) => res.json())
				.catch((err) => console.error(err));
			if (res.status === 200) {
				// display the message
				// wait for a bit (500ms)
				setLogin(true);
			}
		} catch (err: Error | any) {
			setErrors({ form: err });
		} finally {
			setLoading(false);
		}
	};

	return (
		<form onSubmit={handleSubmit} className="space-y-4">
			{errors.form && (
				<motion.div
					initial={{ opacity: 0, y: -10 }}
					animate={{ opacity: 1, y: 0 }}
					className={`p-3 ${
						theme === "light" ? `bg-red-50` : `dark:bg-red-900/20`
					} text-red-500 dark:text-red-400 text-sm border outline shadow-md rounded-lg `}
				>
					{errors.form}
				</motion.div>
			)}
			{/* Progress Step */}
			<ProgressSteps currStep={currStep} />
			<AnimatePresence mode="wait">
				{currStep === `account` && (
					<AccountStep
						errors={errors}
						formData={formData}
						setFormData={setFormData}
						isLoading={isLoading as boolean}
						usernameRef={usernameRef as React.RefObject<HTMLInputElement>}
						showPassord={showPassord}
						showConfirmPassword={showConfirmPassword}
						setShowPassword={setShowPassword}
						setShowConfirmPassword={setShowConfirmPassword}
					/>
				)}
				{currStep === `profile` && (
					<ProfileStep
						errors={errors}
						formData={formData}
						setFormData={setFormData}
						isLoading={isLoading as boolean}
						fullNameRef={fullNameRef as React.RefObject<HTMLInputElement>}
					/>
				)}
				{currStep === `review` && (
					<ReviewStep
						errors={errors}
						formData={formData}
						setFormData={setFormData}
						isLoading={isLoading as boolean}
					/>
				)}
			</AnimatePresence>
			<div className="flex justify-between mt-6">
				{currStep !== `account` ? (
					<motion.div whileHover={{ scale: 1.02 }} whileTap={{ scale: 0.98 }}>
						<Button
							type="button"
							variant={`outline`}
							onClick={() => handleBack()}
							disabled={isLoading}
							className="cursor-pointer outline border shadow-md rounded-lg"
						>
							<ChevronLeft className="mr-2 h-4 w-4" />
							Back
						</Button>
					</motion.div>
				) : (
					<div></div>
				)}

				{currStep !== `review` && (
					<motion.div whileHover={{ scale: 1.02 }} whileTap={{ scale: 0.98 }}>
						<Button
							type="button"
							onClick={() => handleNext()}
							disabled={isLoading}
							className="cursor-pointer outline border shadow-md rounded-lg items-center justify-between flex"
						>
							Next
							<ChevronRight className="mr-2 h-4 w-4" />
						</Button>
					</motion.div>
				)}
				{currStep === `review` && (
					<motion.div whileHover={{ scale: 1.02 }} whileTap={{ scale: 0.98 }}>
						<Button
							type="submit"
							disabled={isLoading}
							className="cursor-pointer outline border shadow-md rounded-lg"
						>
							{isLoading ? (
								<>
									<Loader2 className="mr-2 h-4 w-4 animate-spin" />
									Creating account...``
								</>
							) : (
								`Create Account`
							)}
						</Button>
					</motion.div>
				)}
			</div>
		</form>
	);
};

export default Register;
