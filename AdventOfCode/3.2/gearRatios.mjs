'use strict';

const numberRegex = /(\d+)/g;
const starRegex = /\*/g;
const endsWithNumberRegex = /\d+$/;
const startsWithNumberRegex = /^\d+/;

const getStarsFromLine = (line) => {
	const starIndexes = [];
	let match;
	while ((match = starRegex.exec(line)) !== null) {
		starIndexes.push(match.index);
	}
	return starIndexes;
}

const getAdjacentNumbers = (starIndex, line, lineBefore, lineAfter) => {
	const previousPart = line.substring(0, starIndex);
	const nextPart = line.substring(starIndex + 1);

	const adjacentNumbers = [];

	const numberBefore = endsWithNumberRegex.exec(previousPart);
	const numberAfter = startsWithNumberRegex.exec(nextPart);

	if (numberBefore)
		adjacentNumbers.push(parseInt(numberBefore[0]));
	if (numberAfter)
		adjacentNumbers.push(parseInt(numberAfter[0]));

	let match;
	while ((match = numberRegex.exec(lineBefore)) !== null) {
		const numberStartIndex = match.index;
		const numberEndIndex = numberStartIndex + match[0].length - 1;
		if (starIndex >= numberStartIndex - 1 && starIndex <= numberEndIndex + 1)
			adjacentNumbers.push(parseInt(match[0]));
	}
	while ((match = numberRegex.exec(lineAfter)) !== null) {
		const numberStartIndex = match.index;
		const numberEndIndex = numberStartIndex + match[0].length - 1;
		if (starIndex >= numberStartIndex - 1 && starIndex <= numberEndIndex + 1)
			adjacentNumbers.push(parseInt(match[0]));
	}

	return adjacentNumbers;
};

const getGearRatios = (input) => {
	const lines = input.split('\n');
	let returnArray = [];
	lines.forEach((line, index) => {
		const starIndexes = getStarsFromLine(line);
		const lineBefore = index - 1 < 0 ? '.'.repeat(line.length) : lines[index - 1];
		const lineAfter = index + 1 > lines.length - 1 ? '.'.repeat(line.length) : lines[index + 1];

		starIndexes.forEach(starIndex =>
			returnArray.push({
				line: index,
				index: starIndex,
				adjacentNumbers: getAdjacentNumbers(starIndex, line, lineBefore, lineAfter)
			}))
	});
	return returnArray.filter(x => x.adjacentNumbers.length == 2).map(x => x.adjacentNumbers[0] * x.adjacentNumbers[1]);
}

export { getGearRatios };