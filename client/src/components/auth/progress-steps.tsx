import { ICurrSteps } from "@/types/ICurrSteps";
import { step } from "@/types/stepType";
import { useTheme } from "next-themes";
import React from "react";

const ProgressSteps = ({ currStep }: { currStep: step }) => {
	const { theme } = useTheme();
	const steps: ICurrSteps[] = [
		{ step: `account`, label: `Account` },
		{ step: `profile`, label: `Profile` },
		{ step: `review`, label: `Socials` },
	];

	return (
		<div className="flex items-center justify-between mb-6">
			<div className="w-full flex items-center">
				{steps.map((item, idx) => (
					<React.Fragment key={idx}>
						<div
							className={`w-8 h-8 rounded-full flex items-center justify-center border outline shadow-md ${
								currStep === item.step
									? `bg-primary ${
											theme === "light" ? "text-white" : "text-black"
									  }`
									: currStep > item.step
									? `bg-primary ${
											theme === `light` ? `text-white` : `text-black`
									  }`
									: `bg-secondary text-gray-600 dark:text-gray-400`
							}`}
						>
							{idx + 1}
						</div>
						{idx < steps.length - 1 && (
							<div
								className={`flex-1 h-1 ${
									currStep > item.step ? `bg-primary` : `bg-primary/50`
								}`}
							></div>
						)}
					</React.Fragment>
				))}
			</div>
		</div>
	);
};

export default ProgressSteps;
