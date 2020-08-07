$(document).ready(function () {

	const previousButton = $("#previous");
	const nextButton = $("#next");
	const completeButton = $("#complete");
	const question = $("#question");
	const options = $("#options");
	const questionId = $("#questionId");
	const questionType = $("#questionType");
	const startButton = $("#start");
	const startPage = $("#startPage");
	const quPage = $("#quPage");
	const summaryPage = $("#summary-page");
	let isLastQuestion = false;
	let counter = 0;
	let questionTypeEnum = {
		textAnswer : 0,
		mcqAnswer: 1
	};

	summaryPage.hide();

	const questionSetUp = (qstn, optionsElement) => {

		isLastQuestion = qstn.isLastQuestion;
		questionArrangement(qstn.qu);
		optionsElement.children().remove();
		questionId.val(qstn.id);
		questionType.val(qstn.questionType);
		if (qstn.questionType === questionTypeEnum.mcqAnswer) {
			optionArrangerment(qstn.options, optionsElement, qstn.mcqAnswer);
		} else {
			textArrangement(qstn.textAnswer === undefined || qstn.textAnswer === null ? 
				'' : qstn.textAnswer);
		}

		if (isLastQuestion) {
			nextButton.hide();
			completeButton.show();
		}
	}

	const questionArrangement = (textQuestion) => {
		question.html('Q: '+ textQuestion);
	}

	const optionArrangerment = (quOptions, optionsElement, mcqAnswer) => {
		for (i = 0; i < quOptions.length; i++) {
			optionsElement.append($('<span><input type="radio" name="option" id=' + quOptions[i].item1 + ' value=' + quOptions[i].item1 + '>' + quOptions[i].item2 + '</input><br /></span>'));
		}
		if(mcqAnswer) {
			$("input[name='option'][id='"+ mcqAnswer.item1 +"']").prop("checked", true);
		}
	}

	const textArrangement = ans => {
		const width = (window.innerWidth > 0) ? window.innerWidth : screen.width;
		const cols = width < 600 ? width/ 15 : 50;
		options.append($('<textarea type="textarea" class="textarea" rows="4" cols="'+ cols +'" name="answer" id="textAnswer"></textarea><br />'));
		$('#textAnswer').val(ans);
	}

	const showPrevBtn = () => {
		if (counter >= 1) {
			previousButton.show();
		}
	}

	const hidePrevBtn = () => {
		if (counter <= 1) {
			previousButton.hide();
		}
	}
	

	const getQuestion = () => {
		$.ajax({
			type: "GET",
			url: "https://localhost:44325/api/quiz/" + counter,
			success: function (result) {
				questionSetUp(result, options);
			},
			error: function (result) {
				alert(result);
			}
		});
	}

	const putQuestion = () => {
		const quData = {
			id: parseInt(questionId.val()),
			mcqAnswerId: parseInt($("input[name='option']:checked").val()),
			textAnswer: $("#textAnswer") !== undefined ? $("#textAnswer").val(): '',
			questionType: parseInt(questionType.val())
		}
		$.ajax({
			type: "PATCH",
			url: "https://localhost:44325/api/quiz/" + counter,
			contentType: "application/json",
			data: JSON.stringify(quData),
			success: function () {
				
				if(isLastQuestion) {
					quPage.remove();
					getAllQuestions();
				} else {
					++counter;
					showPrevBtn();
					getQuestion(counter);
				}
			},
			error: function (error) {
				let errorMessage;
				(quData.questionType == questionTypeEnum.mcqAnswer) ? 
					errorMessage = ($.parseJSON(error.responseText).errors.McqAnswerId)
					: errorMessage = ($.parseJSON(error.responseText).errors.TextAnswer); 
					
				alert(errorMessage.map(e => e.replace(' Id', '')));
			}
		});
	}
	
	const getAllQuestions = () => {
		$.ajax({
			type: "GET",
			url: "https://localhost:44325/api/quiz/",
			success: function (results) {
				summaryPage.show();
				let newContainer = summaryPage.find('#question-answer').clone();
				summaryPage.find('#question-answer').remove();
				results.map(result => {
					newContainer = newContainer.clone();
					newContainer.find('#summary-question').html('Q: '+ result.qu)
					newContainer.find('#summary-answer').html('Answer: '+ result.answer)
					newContainer.appendTo(summaryPage);
				})
			},
			error: function (result) {
				alert(result);
			}
		});
	}

	startButton.click(function (e) {
		startPage.hide();
		quPage.show();
		e.preventDefault();
		++counter;
		hidePrevBtn();
		getQuestion(counter);
		completeButton.hide();
	});

	previousButton.click(function (e) {
		--counter;
		e.preventDefault();
		getQuestion(counter);
		if (counter === 1) {
			previousButton.hide();
		}
		if (isLastQuestion) {
			nextButton.show();
			completeButton.hide();
		}
	});

	nextButton.click(function (e) {
		e.preventDefault();
		putQuestion();
	});

	completeButton.click(function(e) {
		putQuestion();
	});
});