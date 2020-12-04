(open 'cli-vector)

(define myCommand (lambda (args)
	(display (get-value args "query"))

	0
))

(register-command 
	"my-command"
	"Do Something"
	"todo --query "
	myCommand
)