(open 'cli-vector 'ObjectModel 'conversion)

(define myCommand (lambda (args)
	(display (get-value args "query"))

	0
))

(register-command 
	"my-command"
	"Do Something"
	"my-command --query "
	myCommand
)

(define myImport 
	(lambda args
		(define obj (make-object))
		(set-property obj 'name "scheme test")
		(set-property obj 'content (binary->string (car args)))

		obj
	)
)

(register-importer '.t myImport)