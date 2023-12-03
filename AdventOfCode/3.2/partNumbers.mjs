'use strict';

const numberRegex = /(\d+)/g;
const justDotsRegex = /^\.+$/;

const getNumbersFromLine = (line) => {
	const numbersWithIndexes = [];
	let match;
	while ((match = numberRegex.exec(line)) !== null) {
		numbersWithIndexes.push({ number: match[0], index: match.index });
	}
	return numbersWithIndexes;
};

const isSymbolAdjacent = (numberWithIndex, line, lineBefore, lineAfter) => {
	const { number, index } = numberWithIndex;
	const previousSymbol = index - 1 < 0 ? '.' : line[index - 1];
	const nextSymbol = index + number.length > line.length - 1 ? '.' : line[index + number.length];

	const lineBeforePart = lineBefore.substring(index - 1, index + number.length + 1);
	const lineAfterPart = lineAfter.substring(index - 1, index + number.length + 1);

	return previousSymbol !== '.' || nextSymbol != '.' || !justDotsRegex.test(lineBeforePart) || !justDotsRegex.test(lineAfterPart);
};

const getPartNumbers = (input) => {
	const lines = input.split('\r\n');
	let returnArray = [];
	lines.forEach((line, index) => {
		const partNumbersWithIndexes = getNumbersFromLine(line);
		const lineBefore = index - 1 < 0 ? '.'.repeat(line.length) : lines[index - 1];
		const lineAfter = index + 1 > lines.length - 1 ? '.'.repeat(line.length) : lines[index + 1];

		partNumbersWithIndexes.forEach(numberWithIndex =>
			returnArray.push({
				number: numberWithIndex.number,
				isSymbolAdjacent: isSymbolAdjacent(numberWithIndex, line, lineBefore, lineAfter)
			}))
	});
	return returnArray.filter(x => x.isSymbolAdjacent).map(x => parseInt(x.number, 10));
}

export { getPartNumbers };