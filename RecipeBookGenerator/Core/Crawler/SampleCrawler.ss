(open 'ObjectModel)

(define mycrawler (lambda (url) (
	(define result (make-object))
	(set-property result 'title "hello scheme")

	result
))

(register-crawler mycrawler)