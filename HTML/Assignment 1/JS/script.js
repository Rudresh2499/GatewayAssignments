function initialValues()
{
	document.getElementById("article").setAttribute("style","display: none");
	document.getElementById("punchline").setAttribute("style","top: 82%");
	document.getElementById("articleButton").setAttribute("style","top: 82%");
}

function readMore()
{
	var elementObj = document.getElementById("article");
	var button = document.getElementById("readMoreButton");
	if(elementObj.style.display === "none")
	{
		elementObj.style.display = "block";
		document.getElementById("punchline").setAttribute("style","top: 50%");
		document.getElementById("articleButton").setAttribute("style","top: 50%");
		button.innerHTML = 'Read Less';
	}
	else
	{
		elementObj.style.display = "none";
		document.getElementById("punchline").setAttribute("style","top: 82%");
		document.getElementById("articleButton").setAttribute("style","top: 82%");
		button.innerHTML = 'Read More...';
	}
}