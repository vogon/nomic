var codeMirrorTextarea = document.getElementById("codemirror-target");
var codeMirror = undefined;

$(function() {
		codeMirror = CodeMirror.fromTextArea(codeMirrorTextarea, {
			mode:  		 "python",
			indentUnit:  4,
			lineNumbers: true
		});
	});